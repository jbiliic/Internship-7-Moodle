using Microsoft.Extensions.DependencyInjection;
using Moodle.Application.DTO;

internal class MenuRouter
{
    private readonly IServiceProvider _provider;

    public MenuRouter(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task NavigateTo<TMenu>(UserDTO user)
        where TMenu : IMenu
    {
        var menu = _provider.GetRequiredService<TMenu>();
        await menu.Show(user);
    }
}
