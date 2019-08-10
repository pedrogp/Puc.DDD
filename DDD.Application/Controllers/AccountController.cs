using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Domain.Entities;
using DDD.Service.Services;
using DDD.Service.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private BaseService<Account> accountService = new BaseService<Account>();
        private BaseService<User> userService = new BaseService<User>();

        [HttpPost]
        public IActionResult Post([FromBody]Account item)
        {
            try
            {
                accountService.Post<AccountValidator>(item);

                return new ObjectResult(item.Id);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]Account item)
        {
            try
            {
                accountService.Put<AccountValidator>(item);

                return new ObjectResult(item);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                accountService.Delete(id);

                return new NoContentResult();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("{accountId}/Debit/{amount}")]
        public IActionResult Debit(int accountId, decimal amount)
        {
            try
            {
                var account = accountService.Get(accountId);

                account.Debit(amount);

                accountService.Put<AccountValidator>(account);

                return new ObjectResult("OK");
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("{accountId}/Credit/{amount}")]
        public IActionResult Credit(int accountId, decimal amount)
        {
            try
            {
                var account = accountService.Get(accountId);

                account.Credit(amount);

                var user = userService.Get(account.UserId);

                if (amount > 50000)
                {
                    //Aviso à COAF sobre movimentações acima do limite de 50000 reais
                    Console.WriteLine($"Movimentação de crédito de R${amount} com o CPF {user.Cpf}");
                }

                accountService.Put<AccountValidator>(account);

                return new ObjectResult("OK");
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return new ObjectResult(accountService.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return new ObjectResult(accountService.Get(id));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}