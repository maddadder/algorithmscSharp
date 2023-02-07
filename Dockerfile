FROM mcr.microsoft.com/dotnet/sdk:7.0 as base

# Copy everything else and build
COPY ./ /opt/blazorapp
WORKDIR /opt/blazorapp

RUN ["dotnet","publish","./BlazorHosted/Server/BlazorHosted.Server.csproj","-o","./outputs" ]

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as run
COPY --from=base /opt/blazorapp/outputs /opt/blazorapp/outputs
WORKDIR /opt/blazorapp/outputs
ENTRYPOINT ["dotnet", "BlazorHosted.Server.dll"]