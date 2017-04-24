Simple example of running a .NET core app in a Linux Docker container
=================

A simple example of a .NET core app that connects to a MySQL database, substitute the credentials in `Startup.json` as illustrated in the following lines.

```
            // Substitute credentials - we will use No SSL
            MySqlConnection connection = new MySqlConnection {
                ConnectionString = "server=<server-address>;user id=<user>;<password>;SSL Mode=None;persistsecurityinfo=True;port=3306;database=<dbname>"
            };
```

We will not be using SSL just for illustration.`


The following command will download the dependencies.

```
dotnet restore
```

Optionally build.

```
dotnet build
```

Run the .NET core app

```
dotnet run
```

Which should output something like

```
Project dotnetcore-docker (.NETCoreApp,Version=v1.0) was previously compiled. Skipping compilation.
id		state		population
Hosting environment: Production
Content root path: /Users/raghavansrinivas/work/src/dotnetcore-docker/bin/Debug/netcoreapp1.0
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.
```

Running the following command

```
curl -L localhost:5000
```

Will output

```
3		Mass		11111
is the last row
```

Now publish the app

```
dotnet publish
```


We will Dockerize the image.

```
docker build -t dotnetcore-docker .
```

This uses the binaries based on the `publish` directory and output something like

```
Sending build context to Docker daemon 6.044 MB
Step 1 : FROM microsoft/dotnet:1.0.0-core
 ---> 2e2f5fffbe3c
Step 2 : WORKDIR /app
 ---> Using cache
 ---> b1a64821f2ed
Step 3 : COPY bin/Debug/netcoreapp1.0/publish /app
 ---> Using cache
 ---> 5c5dba9b2e37
Step 4 : ENV ASPNETCORE_URLS http://*:5000
 ---> Using cache
 ---> 8be31a25b32b
Step 5 : EXPOSE 5000
 ---> Using cache
 ---> 0a3829356198
Step 6 : ENTRYPOINT dotnet dotnetcore-docker.dll
 ---> Using cache
 ---> f8354c8b4136
Successfully built f8354c8b4136
```

Run the Docker image as below which runs the image as a daemon.

```
docker run -d -p 5000:5000 dotnetcore-docker
```

Running the following command

```
docker ps
```

Shows the image running with the port forwarding details.

```
docker ps   
CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS              PORTS                    NAMES
2b7a9f0b39b5        dotnetcore-docker   "dotnet dotnetcore-do"   41 seconds ago      Up 39 seconds       0.0.0.0:5000->5000/tcp   gloomy_darwin
```

The output from the command

```
curl -L localhost:5000
```

Should yield the same output as we saw before.


```
3		Mass		11111
is the last row
```
