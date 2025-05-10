using Microsoft.Playwright.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTests
{
    public class BlazorPageTest : PageTest
    {
        public static readonly Uri RootUri = new("https://reallybad.tech/");
    }
}
