using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

namespace PlaywrightTests;

public class E2E : PageTest
{
    [Fact]
    public async Task LoginButtonTakesYouFromBrowsePageToLoginPage()
    {
        await Page.GotoAsync("https://reallybad.tech/");

        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();

        await Expect(Page).ToHaveURLAsync("https://reallybad.tech/Login");
    }

    [Fact]
    public async Task LoginAsAdmin()
    {
        await Page.GotoAsync("https://reallybad.tech/Login");

        await Page.GetByLabel("Username").FillAsync("alv");
        await Page.GetByLabel("Password").FillAsync("crz");

        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();

        await Expect(Page).ToHaveURLAsync("https://reallybad.tech/AdminPanel");
        await Page.GetByText("Greetings, administrator!").IsVisibleAsync();

        //await Page.GetByRole(AriaRole.Link, new() { Name = "Admin dashboard" }).ClickAsync();
    }
}