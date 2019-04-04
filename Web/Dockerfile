FROM microsoft/aspnetcore:lts
COPY dist /app
WORKDIR /app
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "Web.dll"]