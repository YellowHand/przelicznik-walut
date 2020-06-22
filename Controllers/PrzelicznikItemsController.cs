using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrzelicznikWalut.Model;

namespace PrzelicznikWalut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrzelicznikItemsController : ControllerBase
    {

        [HttpPost]
        public async Task<string> PostPrzelicznikItem(PrzelicznikItem przelicznikItem)
        {
            try
            {
                return new PrzelicznikService().Przelicz(przelicznikItem);
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }

    }
}
