using Flashcards.Api.Helpers;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.Commands.CreateFlashcard;
using Flashcards.Application.Features.FlashcardsFeature.Commands.DeleteFlashcard;
using Flashcards.Application.Features.FlashcardsFeature.Commands.UpdateFlashcard;
using Flashcards.Application.Features.FlashcardsFeature.DTOs;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Application.Features.FlashcardsFeature.Queries.GetFlashcardById;
using Flashcards.Application.Features.FlashcardsFeature.Queries.GetRandomFlashcard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashcardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FlashcardsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet("get-random-flashcard/{flashcardListId:guid}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<OperationResult<FlashcardResponseDto>>> GetRandomFlashcard(
            Guid flashcardListId,
            CancellationToken cancellationToken)
        {
            var query = new GetRandomFlashcardQuery(flashcardListId);

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("create-flashcard")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<CreateFlashcardResponseDto>> CreateFlashcard(
            [FromBody] CreateFlashcardDto dto,
            CancellationToken cancellationToken)
        {
            var userId = UserHelper.GetCurrentUserId(User);

            var command = new CreateFlashcardCommand(dto, userId);

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPatch("{flashcardId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<OperationResult<bool>>> UpdateFlashcard(
            Guid flashcardId,
            [FromBody] UpdateFlashcardDto dto,
            CancellationToken cancellationToken)
        {
            var userId = UserHelper.GetCurrentUserId(User);

            var command = new UpdateFlashcardCommand(flashcardId, dto, userId);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{flashcardId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<OperationResult<bool>>> DeleteFlashcard(Guid flashcardId, CancellationToken cancellationToken)
        {
            var userId = UserHelper.GetCurrentUserId(User);

            var command = new DeleteFlashcardCommand(flashcardId, userId);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{flashcardId:guid}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<OperationResult<FlashcardResponseDto>>> GetFlashcardById(
            Guid flashcardId,
            CancellationToken cancellationToken)
        {
            var userId = UserHelper.GetCurrentUserId(User);

            var query = new GetFlashcardByIdQuery(flashcardId, userId);

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsSuccess)
                return Ok(result);

            return NotFound(result);
        }
    }
}
