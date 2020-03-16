
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cqrs_Test.Application.UseCase.CustomerPaymentCard.Command.DeleteCustomerPaymentCard;
using cqrs_Test.Application.UseCase.CustomerPaymentCard.Command.PostCustomerPaymentCard;
using cqrs_Test.Application.UseCase.CustomerPaymentCard.Command.PutCustomerPaymentCard;
using cqrs_Test.Application.UseCase.CustomerPaymentCard.Queries.GetCustomerPaymentCard;
using cqrs_Test.Application.UseCase.CustomerPaymentCard.Queries.GetCustomerPaymentCards;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cqrs_Test.Presenter.Controller
{

    [ApiController]
    //[Authorize]
    [Route("customer_pc")]
    public class CPCController : ControllerBase
    {
        private IMediator meciater;

        public CPCController(IMediator mediatr)
        {
            meciater = mediatr;
        }


        [HttpGet]
        public async Task<ActionResult<GetCustomerPaymentCardsDto>> GetAsync()
        {
            var result = new GetCustomerPaymentCardsQuery();
            return Ok(await meciater.Send(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int ID)
        {
            var result = new GetCustomerPaymentCardQuery(ID);
            var wait = await meciater.Send(result);
            return wait != null ? (IActionResult)Ok(wait) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostCustomerPaymentCardCommand data)
        {
            var result = await meciater.Send(data);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int ID, PutCustomerPaymentCardCommand data)
        {
            data.Dataa.Attributes.id = ID;
            var result = await meciater.Send(data);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int ID)
        {
            var command = new DeleteCustomerPaymentCardCommand(ID);
            var result = await meciater.Send(command);
            return result != null ? (IActionResult)Ok(new { Message = "success" }) : NotFound(new { Message = "not found" });
        }


    }
}
