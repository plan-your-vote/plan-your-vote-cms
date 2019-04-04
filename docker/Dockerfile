FROM microsoft/dotnet:sdk AS build-env
COPY dist /app
WORKDIR /app
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "Web.dll"]



