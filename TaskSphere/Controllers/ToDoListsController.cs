﻿using Microsoft.AspNetCore.Mvc;
using TaskSphere.Models;

namespace TaskSphere.Controllers;

public class ToDoListsController : Controller
{
    private static List<ToDoList> _toDoLists = new List<ToDoList>()
    {
        new ToDoList {ToDoListId = 1, TaskName = "Criar Controller", TaskDescription = "Criação do Controller", TaskType = "Work"},
        new ToDoList {ToDoListId = 2, TaskName = "Academia", TaskDescription = "Ir a Academia", TaskType = "Saúde"},
        new ToDoList {ToDoListId = 3, TaskName = "Projeto SpringBoot", TaskDescription = "Projeto Faculdade", TaskType = "Faculdade"},
        new ToDoList {ToDoListId = 4, TaskName = "Projeto SpringBoot Longo", TaskDescription = "Projeto Faculdade", TaskType = "Faculdade"},

    };

    public IActionResult Index()
    {
        return View(_toDoLists);
    }

    public IActionResult Search(string search, bool? status)
    {
        var items = from i in _toDoLists select i;

        if (!string.IsNullOrEmpty(search))
        {
            items = items.Where(i => i.TaskName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        if(status.HasValue)
        {
            items = _toDoLists.Where(t => t.IsCompleted == status.Value);
        }

        return View("Index", items.ToList());
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
                existingToDoList.TaskType = toDoList.TaskType;
                existingToDoList.StartDate = toDoList.StartDate;
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

    public IActionResult ToggleTaskStatus(int id)
    {
        var task = _toDoLists.FirstOrDefault(t => t.ToDoListId == id);
        if (task != null)
        {
            task.ToggleTaskStatus();
            task.SetEndDate();
        } else
        {
            task.EndDate = null;
        }


        return RedirectToAction("Index");
    }

}
