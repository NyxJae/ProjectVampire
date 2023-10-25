local AdminModel = require("AdminModel");

local Admin = {};
Admin.InputString = "";
Admin.ToolbarIndex = 0;
Admin.ToolbarMenus = { "正在进行", "已完成" };
Admin.TOOLBAR_TODOS_INDEX = 0;
Admin.TOOLBAR_FINISHED_INDEX = 1;

Admin.LabelTodo = FluentGUIStyle.Label().FontBold().FontSize(12);
Admin.Label12Style = FluentGUIStyle.Label().FontBold().FontSize(12);
Admin.LabelHeader = FluentGUIStyle.Label().FontBold().FontSize(20);
Admin.ScrollPos = Vector2Zero;
local EndFrameAction = {};

function Admin:OnShow()
    AdminModel:Load();

    if User.Logined then
        local form = CreateForm();
        form:AddField("username", User.Username.Value);
        form:AddField("password", User.Password.Value);

        Post("https://api.liangxiegame.com/qf/v5/package/list", form, function(responseText)
            local obj = ParseToJSONObject(responseText);
            if obj["code"].IntValue == 1 then
                AdminModel.Repositories = {};
                local repositoriesJsonObject = obj["data"]["repositories"];
                local length = repositoriesJsonObject.Count;
                for i = 0, length - 1 do
                    if repositoriesJsonObject[i]["accessRight"].StringValue == "private" then
                        local repository = {
                            name = repositoriesJsonObject[i]["name"].StringValue;
                        }
                        AdminModel.Repositories[#AdminModel.Repositories + 1] = repository;
                    end
                end
            end
        end);
    else
        Post("https://api.liangxiegame.com/qf/v5/package/list", CreateForm(), function(responseText)
            local obj = ParseToJSONObject(responseText);
            if obj["code"].IntValue == 1 then
                --AdminModel.Repositories = obj["data"]["repositories"];
            end
        end);
    end

end

function Admin:OnGUI()

    GUILayout.Label("待办事项", Admin.Label12Style.Value)
    GUILayout.BeginVertical(GUIStyleBox);
    Admin.ToolbarIndex = GUILayout.Toolbar(Admin.ToolbarIndex, Admin.ToolbarMenus);

    if Admin.ToolbarIndex == Admin.TOOLBAR_TODOS_INDEX then
        GUILayout.BeginHorizontal(GUIStyleBox);
        Admin.InputString = EditorGUILayout.TextField(Admin.InputString);

        if not (Admin.InputString == "") then
            if GUILayout.Button("添加") then
                AdminModel:Add(Admin.InputString);
                Admin.InputString = "";
                GUI.FocusControl(nil);
            end
        end
        GUILayout.EndHorizontal();

        length = #AdminModel.TodoList;
        for i = length, 1, -1 do
            GUILayout.BeginHorizontal(GUIStyleBox);
            GUILayout.Label(AdminModel.TodoList[i].Title, Admin.LabelTodo.Value);

            if i ~= length and GUILayout.Button("↑", GUILayout.Width(20)) then
                AdminModel:Change(i, i + 1);
            end
            if i ~= 1 and GUILayout.Button("↓", GUILayout.Width(20)) then
                AdminModel:Change(i, i - 1);
            end

            if GUILayout.Button("完成", GUILayout.Width(100)) then
                local index = i;
                EndFrameAction[#EndFrameAction + 1] = function()
                    AdminModel:Finish(index);
                end

            end
            GUILayout.EndHorizontal();
        end
    else
        local length = #AdminModel.FinishedTodos;
        for i = 1, length do
            GUILayout.BeginHorizontal(GUIStyleBox);
            GUILayout.Label(AdminModel.FinishedTodos[i], Admin.Label12Style.Value);
            if GUILayout.Button("删除", GUILayout.Width(100)) then
                AdminModel:Delete(i);
            end
            GUILayout.EndHorizontal();
        end
    end

    GUILayout.EndVertical();
    GUILayout.Space(10);

    if GUILayout.Button("打開窗口") then
        MoonSharpEditorWindow.CreateWithTable({
            OnGUI = function()
                print("On Window GUI")
            end
        }).Show();
    end

    GUILayout.Label("包后台管理", Admin.Label12Style.Value);

    if AdminModel.Repositories then
        GUILayout.BeginVertical(GUIStyleBox);
        Admin.ScrollPos = GUILayout.BeginScrollView(Admin.ScrollPos);
        local length = #AdminModel.Repositories;
        for i = 1, length do
            GUILayout.Label(AdminModel.Repositories[i].name);
        end
        GUILayout.EndScrollView();
        GUILayout.EndVertical(GUIStyleBox);
    else
        GUILayout.Label("加载中...")
    end

    for i = 1, #EndFrameAction do
        EndFrameAction[i]();
        EndFrameAction[i] = nil;
    end
end

function Admin:OnHide()
end

return Admin;