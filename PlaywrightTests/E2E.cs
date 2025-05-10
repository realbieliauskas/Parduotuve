using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

namespace PlaywrightTests;

public class E2E : BlazorPageTest
{
    private string pageUrl = $"{RootUri.AbsoluteUri}";

    [Fact]
    public async Task LoginButton_TakesYou_From_BrowsePage_To_LoginPage()
    {
        await Page.GotoAsync(pageUrl);

        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();

        await Expect(Page).ToHaveURLAsync($"{pageUrl}Login");
    }

    [Fact]
    public async Task LoginAsAdmin()
    {
        await Page.PauseAsync();
        await Page.GotoAsync($"{pageUrl}Login");

        await Page.GetByLabel("Username").FillAsync("alv");
        await Page.GetByLabel("Password").FillAsync("crz");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        await Expect(Page).ToHaveURLAsync($"{pageUrl}AdminPanel");
        await Page.GetByText("Greetings, administrator!").IsVisibleAsync();

        await Page.GetByRole(AriaRole.Link, new() { Name = "Admin dashboard" }).ClickAsync();
    }
}