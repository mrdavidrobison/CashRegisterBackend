using Microsoft.AspNetCore.Mvc;

namespace CashRegisterBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CashRegisterController : ControllerBase
    {
        private static decimal cashInRegister = 0;

        public class CashAmount
        {
            public decimal Amount { get; set; }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { CashInRegister = cashInRegister });
        }

        [HttpPost("add")]
        public IActionResult AddCash([FromBody] CashAmount cash)
        {
            decimal amount = cash.Amount;
            cashInRegister += amount;
            return Ok(new { Message = $"Added {amount} to the cash register", NewTotal = cashInRegister });
        }

        [HttpPost("subtract")]
        public IActionResult SubtractCash([FromBody] CashAmount cash)
        {
            decimal amount = cash.Amount;
            if (cashInRegister >= amount)
            {
                cashInRegister -= amount;
                return Ok(new { Message = $"Subtracted {amount} from the cash register", NewTotal = cashInRegister });
            }
            else
            {
                return BadRequest(new { Error = "Insufficient cash in the register" });
            }
        }

        [HttpPost("reset")]
        public IActionResult ResetCashRegister()
        {
            cashInRegister = 0;
            return Ok(new { Message = "Cash register reset", NewTotal = cashInRegister });
        }
    }
}