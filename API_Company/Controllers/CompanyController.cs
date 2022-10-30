
using System;
using System.Collections.Generic;
using API_Company.Models;
using API_Company.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyService _companyService;
        private readonly DeletedCompanyService _deletedService;
        private readonly BlockedService _blockedService;
        private readonly AddressService _addressService;

        public CompanyController(CompanyService companyService, DeletedCompanyService deletedService,
            BlockedService blockedService, AddressService addressService)
        {
            _companyService = companyService;
            _deletedService = deletedService;
            _blockedService = blockedService;
            _addressService = addressService;
            
        }

        [HttpGet]
        public ActionResult<List<Company>> Get() => _companyService.Get();

        [HttpGet("{cnpj}", Name = "GetCompany")]
        public ActionResult<Company> Get(string cnpj)
        {
            cnpj = FormatCnpj(cnpj);

            var company = _companyService.Get(cnpj);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpPost]
        public ActionResult<Company> Create(Company company)
        {
            var cep = company.Address.ZipCode;
            var address = _addressService.GetAdress(cep).Result;

            if (address == null)
            {
                return NotFound();
            }

            else
            {
                address.Number = company.Address.Number;
                address.Complement = company.Address.Complement;
                company.Address = address;
            }


            company.Cnpj = FormatCnpj(company.Cnpj);
            var companyCnpj = _companyService.Get(company.Cnpj); //Verificação: Cnpj existente na db
            if (companyCnpj == null)
            {
                if (CnpjValidator(company.Cnpj) == true)
                {
                   
                    System.TimeSpan tempoAbertura = DateTime.Now.Subtract(company.DtOpen); //Verificação: Tempo de abertura(6 meses)

                    if (tempoAbertura.TotalDays >= 180)
                    {
                        if (company.Status == false)
                        {
                            _companyService.Create(company);
                          
                            return CreatedAtRoute("GetCompany", new { cnpj = company.Cnpj.ToString() }, company);
                        }

                        else
                        {
                            Blocked blocked = new Blocked();
                            blocked.Cnpj = company.Cnpj;
                            blocked.Name = company.Name;
                            blocked.NameOpt = company.NameOpt;
                            blocked.DtOpen = company.DtOpen;
                            blocked.Adress = company.Address;
                            blocked.Aircraft = company.Aircraft;

                            _blockedService.Create(blocked);
                            _companyService.Create(company);
                            _companyService.PostAircraft(company.Aircraft);
                            return CreatedAtRoute("GetCompany", new { cnpj = company.Cnpj.ToString() }, company);
                        }
                    }

                    else
                    {
                        return BadRequest("Tempo de abertura da companhia menor que 6 meses");
                    }
                }

                else
                {
                    return BadRequest("Cnpj inválido");
                }
            }

            else
            {
                return BadRequest("Cnpj já cadastrado");
            }
        }

        [HttpPut]
        public ActionResult<Company> Put(Company company_, string cnpj)
        {


            var company = _companyService.Get(cnpj);
            company.Cnpj = FormatCnpj(cnpj);

            if (company == null)
            {
                return NotFound("Não encontrado");
            }

            else
            {

                if (company_.Id == company.Id || company_.Cnpj == cnpj) //Verificação: Não editar campos unicos
                {
                    if (company_.Id == company.Id)
                    {
                        if (company_.Cnpj == cnpj)
                        {
                            _companyService.Update(company.Cnpj, company_);
                            return Ok(company_);
                        }

                        else
                        {
                            return BadRequest("Impossivel editar o campo Cnpj");
                        }
                    }

                    else
                    {
                        return BadRequest("Impossivel editar o campo Id");
                    }

                }

                else
                {
                    return BadRequest("Impossivel editar o campo Id e Cnpj");
                }

            }

        }

        [HttpDelete]
        public ActionResult<Company> Delete(string cnpj)
        {
            cnpj= FormatCnpj(cnpj);

            Company company = _companyService.Get(cnpj);
            if (company == null)
            {
                return NotFound();
            }

            DeletedCompany deleted = new DeletedCompany();
            deleted.Cnpj = company.Cnpj;
            deleted.Name = company.Name;
            deleted.NameOpt = company.NameOpt;
            deleted.DtOpen = company.DtOpen;
            deleted.Address = company.Address;
            deleted.Aircraft = company.Aircraft;

            _deletedService.Create(deleted);
            _companyService.Remove(company);

            return NoContent();

        }

        private static bool CnpjValidator(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;


            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else

                resto = 11 - resto;

            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static string FormatCnpj(string cnpj)
        {
            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }

    }

}


