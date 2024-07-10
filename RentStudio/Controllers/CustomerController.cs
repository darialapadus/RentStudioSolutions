using Microsoft.AspNetCore.Mvc;
using RentStudio.Models.DTOs;
using RentStudio.Services.AzureService;
using RentStudio.Services.CustomerService;

namespace RentStudio.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly AzureService _azureService;

        public CustomerController(ICustomerService customerService, AzureService azureService)
        {
            _customerService = customerService;
            _azureService = azureService;
        }

        /*[HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _customerService.GetCustomers();
            return Ok(customers);
        }*/

        [HttpGet]
        public IActionResult GetCustomers([FromQuery] FilterCustomerDTO filterCustomerDTO)
        {
            var customers = _customerService.GetCustomers(filterCustomerDTO);
            return Ok(customers);
        }


        [HttpPost]  
        public IActionResult AddCustomer([FromBody] CustomerDTO customerDto)
        {
            _customerService.AddCustomer(customerDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerShortDTO updatedCustomer)
        {
            _customerService.UpdateCustomer(id, updatedCustomer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            _customerService.DeleteCustomer(id);
            return Ok();
        }

        // GROUPBY pentru a grupa clientii in functie de oras.
        [HttpGet("grouped-by-city")]
        public IActionResult GetCustomersGroupedByCity()
        {
            var customersGroupedByCity = _customerService.GetCustomersGroupedByCity();
            return Ok(customersGroupedByCity);
        }

        // WHERE pentru a obtine toti clientii cu un anumit nume.
        [HttpGet("with-first-name/{firstName}")]
        public IActionResult GetCustomersWithFirstName(string firstName)
        {
            var customersWithFirstName = _customerService.GetCustomersWithFirstName(firstName);
            return Ok(customersWithFirstName);
        }

        // JOIN intre Customers si Reservations pentru a obtine informatiile despre clienti impreuna cu datele despre rezervari.
        [HttpGet("with-reservations")]
        public IActionResult GetCustomersWithReservations()
        {
            var customersWithReservations = _customerService.GetCustomersWithReservations();
            return Ok(customersWithReservations);
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("Invalid image file.");

            using (var stream = imageFile.OpenReadStream())
            {
                var imageUrl = await _azureService.UploadImageAsync(stream, imageFile.FileName);
                return Ok($"Image uploaded successfully. URL: {imageUrl}");
            }
        }

        [HttpGet("get-image/{imageName}")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
                return BadRequest("Invalid image name.");

            var imageStream = await _azureService.GetImageAsync(imageName);
            if (imageStream == null)
                return NotFound("Image not found.");

            return File(imageStream, "image/jpeg");
        }

        [HttpPost("upload-pdf")]
        public async Task<IActionResult> UploadPdf(IFormFile pdfFile)
        {
            if (pdfFile == null || pdfFile.Length == 0)
                return BadRequest("Invalid PDF file.");

            using (var stream = pdfFile.OpenReadStream())
            {
                var pdfUrl = await _azureService.UploadPdfAsync(stream, pdfFile.FileName);
                return Ok($"PDF uploaded successfully. URL: {pdfUrl}");
            }
        }

        [HttpGet("get-pdf/{pdfName}")]
        public async Task<IActionResult> GetPdf(string pdfName)
        {
            if (string.IsNullOrEmpty(pdfName))
                return BadRequest("Invalid PDF name.");

            var pdfStream = await _azureService.GetPdfAsync(pdfName);
            if (pdfStream == null)
                return NotFound("PDF not found.");

            return File(pdfStream, "application/pdf");
        }
    }
}


