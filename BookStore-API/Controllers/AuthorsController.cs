using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore_API.Contracts;
using BookStore_API.Data;
using BookStore_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    /// <summary>
    /// Endpoint to Authors
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public AuthorsController(IAuthorRepository authorRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            _authorRepository = authorRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Authors
        /// </summary>
        /// <returns>List of Authors</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthors()
        {

            try
            {
                _logger.LogInfo("GetAuthors() called");
                var authors = await _authorRepository.FindAll();
                var response = _mapper.Map<IList<AuthorDTO>>(authors);
                _logger.LogInfo("GetAuthors() OK");
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"GetAuthors() {e.Message} - {e.InnerException}");
                return StatusCode(500);
            }
            
        }

        /// <summary>
        /// Get an Author by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An Author´s record</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthor(int id)
        {

            try
            {
                _logger.LogInfo($"GetAuthor({id}) called");
                var author = await _authorRepository.FindById(id);
                if (author == null)
                {
                    _logger.LogWarn($"GetAuthor({id}) not found");
                    return NotFound();
                }
                var response = _mapper.Map<AuthorDTO>(author);
                _logger.LogInfo($"GetAuthor({id}) OK");
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"GetAuthor({id}) {e.Message} - {e.InnerException}");
                return StatusCode(500);
            }

        }

        /// <summary>
        /// Creates a Author
        /// </summary>
        /// <param name="authorDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AuthorCreateDTO authorDTO)
        {
            try
            {
                if (authorDTO == null)
                {
                    _logger.LogWarn($"Create() empty");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Create() invalid");
                    return BadRequest(ModelState);
                }

                var author = _mapper.Map<Author>(authorDTO);
                var isSuccess = await _authorRepository.Create(author);
                if (!isSuccess)
                {
                    _logger.LogWarn($"Create() failed");
                    return StatusCode(500);
                }

                _logger.LogInfo($"Create() created");
                return Created("Create", new { author });

            }
            catch (Exception e)
            {
                _logger.LogError($"Create() {e.Message} - {e.InnerException}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates a Author
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorUpdateDTO authorDTO)
        {
            try
            {
                if (authorDTO == null || id < 1 || id != authorDTO.Id)
                {
                    _logger.LogWarn($"Update() empty");
                    return BadRequest(ModelState);
                }

                var isExist = await _authorRepository.isExists(id);
                if (!isExist)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Update() invalid");
                    return BadRequest(ModelState);
                }

                var author = _mapper.Map<Author>(authorDTO);
                var isSuccess = await _authorRepository.Update(author);
                if (!isSuccess)
                {
                    _logger.LogWarn($"Update() failed");
                    return StatusCode(500);
                }

                _logger.LogInfo($"Update() created");
                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError($"Update() {e.Message} - {e.InnerException}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes a Author
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    _logger.LogWarn($"Delete() empty");
                    return BadRequest();
                }

                var isExist = await _authorRepository.isExists(id);
                if (!isExist)
                {
                    return NotFound();
                }

                var author = await _authorRepository.FindById(id);
                var isSuccess = await _authorRepository.Delete(author);
                if (!isSuccess)
                {
                    _logger.LogWarn($"Delete() failed");
                    return StatusCode(500);
                }

                _logger.LogInfo($"Delete() created");
                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError($"Delete() {e.Message} - {e.InnerException}");
                return StatusCode(500);
            }
        }

    }
}
