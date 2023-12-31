---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by liangxie.
--- DateTime: 2023/6/28 17:56
---
local AdminModel = {}

local FINISHED_STORAGE_PATH = "Assets/QFramework/Admin/Editor/finished.json";
local TODOS_STORAGE_PATH = "Assets/QFramework/Admin/Editor/todos.json";

AdminModel.FinishedTodos = {};

AdminModel.TodoList = {{Title="Hello World",Finished=false},{Title="123123123",Finished=false}}
AdminModel.Repositories = nil;

function AdminModel:Add(todo)
    AdminModel.TodoList[#AdminModel.TodoList + 1] = {Title=todo,Finished=false};
    AdminModel:Save();
end

function AdminModel:Finish(index)
    AdminModel.FinishedTodos[#AdminModel.FinishedTodos + 1] = AdminModel.TodoList[index].Title;
    table.remove(AdminModel.TodoList, index);
    AdminModel:Save();
end

function AdminModel:Delete(index)
    table.remove(AdminModel.FinishedTodos, index);
    AdminModel:Save();
end

function AdminModel:Load()
    if File.exists(FINISHED_STORAGE_PATH) then
        local text = File.readAllText(FINISHED_STORAGE_PATH);
        AdminModel.FinishedTodos =  json.parse(text);
    end

    if File.exists(TODOS_STORAGE_PATH) then
        local text = File.readAllText(TODOS_STORAGE_PATH);
        AdminModel.TodoList =  json.parse(text);
    end
end

function AdminModel:Change(a,b)
    local todo = AdminModel.TodoList[a];
    AdminModel.TodoList[a] = AdminModel.TodoList[b];
    AdminModel.TodoList[b] = todo;
    AdminModel:Save();
end

function AdminModel:Save()
    File.writeAllText(FINISHED_STORAGE_PATH,json.serialize(AdminModel.FinishedTodos));
    File.writeAllText(TODOS_STORAGE_PATH,json.serialize(AdminModel.TodoList));
    --AssetDatabase.Refresh();
end

return AdminModel;