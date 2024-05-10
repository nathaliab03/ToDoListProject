using Microsoft.AspNetCore.Mvc;
using TaskSphere.Models;

namespace TaskSphere.Controllers;

public class ToDoListsController : Controller
{
    private static List<ToDoList> _toDoLists = new List<ToDoList>()
    {
        new ToDoList {ToDoListId = 1, TaskName = "Criar Controller", TaskDescription = "Criação do Controller", TaskStatus = "Em Andamento", TaskType = "Work", StartDate = new DateOnly(2024, 2, 25) },
        new ToDoList {ToDoListId = 2, TaskName = "Academia", TaskDescription = "Ir a Academia", TaskStatus = "Em Andamento", TaskType = "Saúde", StartDate = new DateOnly(2024, 2, 25) },
        new ToDoList {ToDoListId = 3, TaskName = "Projeto SpringBoot", TaskDescription = "Projeto Faculdade", TaskStatus = "Concluida", TaskType = "Faculdade", StartDate = new DateOnly(2024, 2, 25) },
        new ToDoList {ToDoListId = 3, TaskName = "Projeto SpringBoot Longo", TaskDescription = "Projeto Faculdade", TaskStatus = "Concluida", TaskType = "Faculdade", StartDate = new DateOnly(2024, 2, 25) },
    };

    public IActionResult Index()
    {
        return View(_toDoLists);
    }

    [HttpGet]
    public IActionResult Create() 
    {
        return View(); 
    }

    [HttpPost]
    public IActionResult Create(ToDoList toDoList)
    {
        if(ModelState.IsValid)
        {
            toDoList.ToDoListId = _toDoLists.Count > 0 ? _toDoLists.Max(l => l.ToDoListId) + 1 : 1;
            _toDoLists.Add(toDoList);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var existingToDoList = _toDoLists.FirstOrDefault(l => l.ToDoListId == id);
        if (existingToDoList == null) 
            return NotFound();

       return View(existingToDoList);
    }

    [HttpPost]
    public IActionResult Edit(ToDoList toDoList)
    {
        if (ModelState.IsValid)
        {
            var existingToDoList = _toDoLists.FirstOrDefault(l => l.ToDoListId == toDoList.ToDoListId);
            if(existingToDoList != null)
            {
                existingToDoList.TaskName = toDoList.TaskName;
                existingToDoList.TaskDescription = toDoList.TaskDescription;
                existingToDoList.TaskStatus = toDoList.TaskStatus;
                existingToDoList.TaskType = toDoList.TaskType;
                existingToDoList.StartDate = toDoList.StartDate;
                existingToDoList.EndDate = toDoList.EndDate;
            }
            return RedirectToAction("Index");
        }

        return View(toDoList);
    }

    public IActionResult Detail(int id)
    {
        var toDoList = _toDoLists.FirstOrDefault(l => l.ToDoListId == id);
        if(toDoList == null)
            return NotFound();

        return View(toDoList);
    }

    public IActionResult Delete(int id)
    {
        var toDoList = _toDoLists.FirstOrDefault(l => l.ToDoListId == id);
        if(toDoList == null)
            return NotFound();

        _toDoLists.Remove(toDoList);

        return RedirectToAction("Index");
    }

    public IActionResult DoingTask() {

        var doingTask = _toDoLists.Where(t => t.TaskStatus == "Em Andamento").ToList();

        return View(doingTask);
    }

    public IActionResult TaskDone()
    {
        var doneTask = _toDoLists.Where(t => t.TaskStatus == "Concluida").ToList();

        return View(doneTask);
    }
}
