FROM  mcr.microsoft.com/dotnet/core/sdk:2.2 as build
WORKDIR /App
ADD . /App
RUN dotnet build
RUN dotnet test
RUN cd /App/App && dotnet publish

FROM  mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /App
EXPOSE 8080
ENV SERVER_HOST "http://0.0.0.0:8080"
ENV WORKSHOP_EVENTSTORE "eventstore"
ENTRYPOINT ["dotnet", "App.dll"]
COPY --from=build /App/App/bin/Debug/netcoreapp2.2/publish/ /App 
