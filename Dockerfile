FROM microsoft/dotnet:1.0.0-core
WORKDIR /app
COPY bin/Debug/netcoreapp1.0/publish /app
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "dotnetcore-docker.dll"]
