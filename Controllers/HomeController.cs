using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using endsem_project.Models;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace endsem_project.Controllers;

public class HomeController : Controller
{
    private readonly TodoDBContext todoDB;

    public HomeController(TodoDBContext todoDB)
    {
        this.todoDB = todoDB;
    }
    public IActionResult Index()
    {
        var data = todoDB.Todo.ToList();
        return View(data);
    }

    public async Task<IActionResult> CreatePrimary(Todo std){
        if (ModelState.IsValid){
            await todoDB.Todo.AddAsync(std);
            await todoDB.SaveChangesAsync();
            TempData["insert"] = "Database successfully updated";
            return RedirectToAction("Index", "Home");
        }
        return View(std);
    }

    public async Task<IActionResult> CreateSecondary(Todo std){
        if (ModelState.IsValid){
            await todoDB.Todo.AddAsync(std);
            await todoDB.SaveChangesAsync();
            TempData["insert"] = "Database successfully updated";
            return RedirectToAction("Index", "Home");
        }
        return View(std);
    }


    public async Task<IActionResult> Edit(int? id, Todo std)
    {
        if (id == null)
        {
            return NotFound();
        }
        var previousData = await todoDB.Todo.FindAsync(id);
        if (ModelState.IsValid)
        {
            if (previousData == null)
            {
                return NotFound();
            }
            if (std.TaskName != " ")
            {
                previousData.TaskName = std.TaskName;
            }
            if (std.TaskTag != null)
            {
                previousData.TaskTag = std.TaskTag;
            }
            if (std.Priority != "0")
            {
                previousData.Priority = std.Priority;
            }
            todoDB.Update(previousData);
            await todoDB.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        ViewBag.previousData = previousData;
        return View(std);
    }

    public async Task<IActionResult> Delete(int? id){
        if (id == null) return NotFound();

        var todoItem = await todoDB.Todo.FindAsync(id);
        if (todoItem == null) return NotFound();

        todoDB.Todo.Remove(todoItem);
        await todoDB.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Complete(int? id){
        if (id == null) return NotFound();

        var todoItem = await todoDB.Todo.FindAsync(id);
        if (todoItem == null) return NotFound();

        todoItem.Priority = "Completed";

        todoDB.Todo.Update(todoItem);
        await todoDB.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
