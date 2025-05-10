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
        // await Page.PauseAsync();
        await Page.GotoAsync(pageUrl);

        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();

        await Expect(Page).ToHaveURLAsync($"{pageUrl}Login");
    }

    [Fact]
    public async Task LoginAsAdmin()
    {
        // await Page.PauseAsync();
        await Page.GotoAsync(pageUrl);
        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username*" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username*" }).FillAsync("alv");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username*" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Password*" }).FillAsync("crz");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        await Page.GetByText("Greetings, administrator!").ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Admin dashboard" }).ClickAsync();
    }

    [Fact]
    public async Task GoToMechaAatroxSkinDescriptionPage_ClickAddToCart2Times_CheckCartFor2Skins_Remove2Skins_CheckForEmptyCartMessage()
    {
        // await Page.PauseAsync();
        await Page.GotoAsync(pageUrl);
        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username*" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username*" }).FillAsync("Jonaitynas");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username*" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Password*" }).FillAsync("jonas");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        await Page.Locator("div:nth-child(2) > .mud-image").ClickAsync();
        await Task.Delay(500);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Add to cart" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Add to cart" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Shopping cart" }).ClickAsync();
        await Page.GetByRole(AriaRole.Cell, new() { Name = "Mecha Aatrox" }).ClickAsync();
        await Page.GetByRole(AriaRole.Cell, new() { Name = "2", Exact = true }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "-" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "-" }).ClickAsync();
        await Page.GetByText("Your shopping cart is empty.").ClickAsync();
    }

    [Fact]
    public async Task SortSkinsByPrice_Then_SortThemByPriceInReverseOrder()
    {
        // await Page.PauseAsync();
        await Page.GotoAsync(pageUrl);
        await Page.GetByText("Champion Name").ClickAsync();
        await Page.GetByText("Price").ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Goth Annie" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Red Riding Annie" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Prom Queen Annie" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Frostfire Annie" })).ToBeVisibleAsync();
        await Expect(Page.Locator("h6").First).ToBeVisibleAsync();
        await Expect(Page.Locator("div:nth-child(2) > .mud-card-actions > .mud-typography")).ToBeVisibleAsync();
        await Expect(Page.Locator("body")).ToContainTextAsync("9.99 €");
        await Expect(Page.Locator("body")).ToContainTextAsync("9.99 €");
        await Expect(Page.Locator("body")).ToContainTextAsync("9.99 €");
        await Expect(Page.Locator("body")).ToContainTextAsync("9.99 €");
        await Page.GetByRole(AriaRole.Checkbox, new() { Name = "Reverse sort" }).CheckAsync();
        await Page.GotoAsync($"{pageUrl}?SortBy=Price&Page=1&Reverse=True&SearchQuery=");
        await Expect(Page.Locator(".mud-image").First).ToBeVisibleAsync();
        await Expect(Page.Locator("div:nth-child(2) > .mud-image")).ToBeVisibleAsync();
        await Expect(Page.Locator("div:nth-child(3) > .mud-image")).ToBeVisibleAsync();
        await Expect(Page.Locator("div:nth-child(4) > .mud-image")).ToBeVisibleAsync();
        await Expect(Page.Locator("body")).ToContainTextAsync("99.99 €");
        await Expect(Page.Locator("body")).ToContainTextAsync("99.99 €");
        await Expect(Page.Locator("body")).ToContainTextAsync("99.99 €");
        await Expect(Page.Locator("body")).ToContainTextAsync("99.99 €");
    }
}