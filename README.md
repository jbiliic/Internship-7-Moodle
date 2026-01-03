# Internship-7-Moodle

This is a .NET 8 console application using PostgreSQL (EF Core) and Redis, fully containerized with Docker.

Prerequisites:
- Docker Desktop (includes Docker Compose)

# Running the Application

### Verify Docker is running:
docker version

### From the solution root:

docker compose up -d postgres redis
docker run -it --network moodle_default moodle-app:latest

You will see the interactive menu:
Welcome to Moodle!
1. Login
2. Register
0. Exit

### Stop the application:
Ctrl + C

### Cleanup:
docker compose down -v

### One-command start:
docker compose up --build
