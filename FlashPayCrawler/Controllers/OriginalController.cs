﻿using FlashPayCrawler.Apis;
using Microsoft.AspNetCore.Mvc;

namespace FlashPayCrawler.Controllers
{
    [Route("[controller]")]
    public class OriginalController : BaseController
    {
        protected override BaseApi api { get { return new OriginalApi("Original"); } }
    }
}