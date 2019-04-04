FROM microsoft/aspnetcore:2.0-nanoserver-sac2016
COPY dist /app
WORKDIR /app
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "Web.dll"]