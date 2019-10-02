#msbuild -t:restore -t:clean,build -p:Configuration=Release Naxam.I18n.sln
 nuget pack ./i18n.nuspec