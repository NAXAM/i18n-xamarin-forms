using System;
namespace Naxam.I18n
{
	public interface ILocalizedResourceProvider
	{
		string GetText(string resourceKey, params object[] objects);
	}
}
