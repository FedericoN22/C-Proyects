[README_EN.md](https://github.com/user-attachments/files/23915070/README_EN.md)
# 📝 Task Tracker CLI

A simple yet complete **Task Tracker** built in **C#**, inspired by the
project proposed on
[roadmap.sh](https://roadmap.sh/projects/task-tracker).\
This command-line tool allows you to manage tasks: create, update,
delete, list, and change their status.

Perfect for practicing: - C# programming\
- Argument parsing in CLI applications\
- JSON persistence\
- Basic architecture and separation of concerns\
- Working with Dictionary and List\
- File handling in .NET

------------------------------------------------------------------------

# 🚀 Usage

## 📌 Running the program

From the project folder:

``` bash
dotnet run -- <command> [arguments]
```

> **Note:**\
> The double dash `--` is required so that everything after it is passed
> directly into `args[]` inside `Program.cs`.

------------------------------------------------------------------------

# 📚 Available Commands

## ➕ Add a new task

``` bash
dotnet run -- add "Task description"
```

Example:

``` bash
dotnet run -- add "Learn C#"
```

A new task is created with the **Todo** status by default.

------------------------------------------------------------------------

## ✏️ Update a task description

``` bash
dotnet run -- update <id> "New description"
```

Example:

``` bash
dotnet run -- update 3 "Review .NET documentation"
```

------------------------------------------------------------------------

## ❌ Delete a task

``` bash
dotnet run -- delete <id>
```

Example:

``` bash
dotnet run -- delete 2
```

------------------------------------------------------------------------

## 🔁 Change a task's status

### Mark as **Todo**

``` bash
dotnet run -- mark-todo <id>
```

### Mark as **In Progress**

``` bash
dotnet run -- mark-in-progress <id>
```

### Mark as **Done**

``` bash
dotnet run -- mark-done <id>
```

------------------------------------------------------------------------

# 📋 List tasks

## List **all** tasks

``` bash
dotnet run -- list
```

## List tasks in **Todo** state

``` bash
dotnet run -- list todo
```

## List tasks **In Progress**

``` bash
dotnet run -- list in-progress
```

## List tasks **Done**

``` bash
dotnet run -- list done
```

------------------------------------------------------------------------

# 💾 Data Persistence

Tasks are stored in the following JSON file:

    JsonTask.json

This file is automatically created if it does not exist, and keeps all
tasks persistently across program runs.

------------------------------------------------------------------------

# 🏗 Project Structure

    /Program.cs          → Main CLI controller
    Tarea                → Task model
    DatosTask            → CRUD logic and operations
    JsonFile             → JSON read/write helper
    TareaJson            → Dictionary wrapper
    JsonTask.json        → Persisted task storage

------------------------------------------------------------------------

# 🎯 Future Enhancements (Optional)

-   Add colorized console output\
-   Implement unit tests\
-   Split each class into separate files\
-   Add a `--help` command\
-   Export tasks to CSV or Markdown\
-   Add command auto-completion\
-   Create a GUI or web version

------------------------------------------------------------------------

# 🤝 Contributing

This project is educational and open to improvements, forks, or
extensions.

------------------------------------------------------------------------

# 📬 Contact

Feel free to reach out if you'd like help improving or extending this
CLI.
