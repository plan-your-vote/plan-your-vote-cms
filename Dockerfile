FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Web/Web.csproj ./Web/
COPY VotingModelLibrary/VotingModelLibrary.csproj ./VotingModelLibrary/
WORKDIR /app/Web

RUN dotnet restore

# Copy everything else and build
WORKDIR /app
COPY ./Web/. ./Web/
COPY ./VotingModelLibrary/. ./VotingModelLibrary/
WORKDIR /app/Web
RUN dotnet publish -c Release -o out


# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/Web/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "Web.dll"]