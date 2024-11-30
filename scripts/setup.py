## this script will create folders and projects for all days of Advent of Code 2024
## Day01 is setup manually and the script will copy the Day1 folder and rename it for Day02 to Day25
## I"m using .net 9.0 and c# for the projects

import os
import shutil
import subprocess

# Define the base directory and solution name
base_dir = "../AdventOfCode2024"
solution_name = "AdventOfCode2024.sln"
day1_folder = os.path.join(base_dir, "Day01")

# Ensure the Day01 folder exists
if not os.path.exists(day1_folder):
    raise FileNotFoundError(f"{day1_folder} does not exist. Please create Day01 first.")

# Change to the base directory
os.chdir(base_dir)

# Create the solution file if it doesn't exist
if not os.path.exists(solution_name):
    subprocess.run(["dotnet", "new", "sln", "-n", solution_name])

# Loop to create folders and projects from Day02 to Day25
for day in range(2, 26):
    folder_name = f"Day{day:02}"
    project_name = f"Day{day:02}"
    
    # Copy the Day01 folder and rename it
    shutil.copytree("Day01", folder_name)
    
    # Rename the project inside the copied folder
    old_project_file = os.path.join(folder_name, "Day01.csproj")
    new_project_file = os.path.join(folder_name, f"{project_name}.csproj")
    os.rename(old_project_file, new_project_file)
    
    # Update the project file content to reflect the new project name
    with open(new_project_file, 'r') as file:
        filedata = file.read()
    filedata = filedata.replace("<Project Sdk=\"Microsoft.NET.Sdk\">", f"<Project Sdk=\"Microsoft.NET.Sdk\">\n  <PropertyGroup>\n    <OutputType>Exe</OutputType>\n    <TargetFramework>net9.0</TargetFramework>\n    <RootNamespace>{project_name}</RootNamespace>\n  </PropertyGroup>")
    with open(new_project_file, 'w') as file:
        file.write(filedata)
    
    # Add the project to the solution
    subprocess.run(["dotnet", "sln", solution_name, "add", new_project_file])

print("Folders and projects created successfully.")