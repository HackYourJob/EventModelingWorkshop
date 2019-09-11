FROM  mcr.microsoft.com/dotnet/core/sdk:2.2 as build
ADD . /App
RUN cd /App/App && dotnet publish

FROM  mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /App
EXPOSE 8080
ENV SERVER_HOST "http://0.0.0.0:8080"
ENTRYPOINT ["dotnet", "App.dll"]
COPY --from=build /App/App/bin/Debug/netcoreapp2.2/publish/ /App 
