/****************************************************************************
* Copyright (c) 2016 ~ 2023 liangxie UNDER MIT License
* 
* https://qframework.cn
* https://github.com/liangxiegame/QFramework
* https://gitee.com/liangxiegame/QFramework
****************************************************************************/

#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;


namespace QFramework
{
    public class AlphaToAdmin : EasyEditorWindow, IController, IUnRegisterList
    {
        public static void InitViewState()
        {
            InEditorView.Value = true;
            InFinishView.Value = false;
            InUploadingView.Value = false;
            NoticeMessage.Value = "";
            UpdateResult.Value = "";
        }

        public static BindableProperty<bool> InEditorView = new BindableProperty<bool>(true);
        public static BindableProperty<bool> InFinishView = new BindableProperty<bool>(true);
        public static BindableProperty<bool> InUploadingView = new BindableProperty<bool>(true);
        public static BindableProperty<string> NoticeMessage = new BindableProperty<string>("");
        public static BindableProperty<string> UpdateResult = new BindableProperty<string>("");

        private PackageVersion mPackageVersion;

        private AssetTree mAssetTree;
        private AssetTreeIMGUI mAssetTreeGUI;
        private Vector2 mScrollPosition;

        static string MakeInstallPath()
        {
            var path = MouseSelector.GetSelectedPathOrFallback();

            if (path.EndsWith("/"))
            {
                return path;
            }

            return path + "/";
        }

        private static void MakePackage()
        {
            var path = MouseSelector.GetSelectedPathOrFallback();

            if (!string.IsNullOrEmpty(path))
            {
                if (Directory.Exists(path))
                {
                    var installPath = MakeInstallPath();

                    new PackageVersion
                    {
                        InstallPath = installPath,
                        Version = "v0.0.0",
                        IncludeFileOrFolders = new List<string>()
                        {
                            // 去掉最后一个元素
                            installPath.Remove(installPath.Length - 1)
                        }
                    }.Save();

                    AssetDatabase.Refresh();
                }
            }
        }

        [MenuItem("Assets/@Admin/Make Alpha To Admin")]
        public static void MakeAlpha2Admin()
        {
            var selectObject = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);


            if (selectObject == null || selectObject.Length > 1)
            {
                return;
            }

            if (!EditorUtility.IsPersistent(selectObject[0]))
            {
                return;
            }

            var path = AssetDatabase.GetAssetPath(selectObject[0]);

            if (!Directory.Exists(path))
            {
                return;
            }

            var window = (AlphaToAdmin)GetWindow(typeof(AlphaToAdmin), true);

            window.titleContent = new GUIContent(selectObject[0].name);

            window.position = new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 258, 500);

            window.Show();
        }
        

        public static bool MakeAlpha2AdminCheck()
        {
            return true;
        }


        private void OnEnable()
        {
            var selectObject = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
            
            if (selectObject == null || selectObject.Length > 1)
            {
                return;
            }
            
            var packageFolder = AssetDatabase.GetAssetPath(selectObject[0]);
            
            var files = Directory.GetFiles(packageFolder, "PackageVersion.json", SearchOption.TopDirectoryOnly);
            
            if (files.Length <= 0)
            {
                MakePackage();
            }
            
            mPackageVersion = PackageVersion.Load(packageFolder);
            mPackageVersion.InstallPath = MakeInstallPath();
            
            mPublishVersion = mPackageVersion.Version;
            
            var versionNumbers = mPublishVersion.Split('.');
            var lastVersionNumber = int.Parse(versionNumbers.Last());
            // lastVersionNumber++;
            versionNumbers[versionNumbers.Length - 1] = lastVersionNumber.ToString();
            mPublishVersion = string.Join(".", versionNumbers);
        }

        public override void OnClose()
        {
            this.UnRegisterAll();
        }

        public override void OnUpdate()
        {
        }

        private VerticalLayout RootLayout = null;

        private string mPublishVersion = null;

        protected override void Init()
        {
            InitViewState();

            var hashSet = new HashSet<string>();

            if (mPackageVersion.IncludeFileOrFolders.Count == 0 && mPackageVersion.InstallPath.EndsWith("/"))
            {
                hashSet.Add(mPackageVersion.InstallPath.Remove(mPackageVersion.InstallPath.Length - 1));
            }

            foreach (var packageIncludeFileOrFolder in mPackageVersion.IncludeFileOrFolders)
            {
                hashSet.Add(packageIncludeFileOrFolder);
            }

            mAssetTree = new AssetTree();
            mAssetTreeGUI = new AssetTreeIMGUI(mAssetTree.Root);
            var guids = AssetDatabase.FindAssets(string.Empty);
            int i = 0, l = guids.Length;
            for (; i < l; ++i)
            {
                mAssetTree.AddAsset(guids[i], hashSet);
            }

            RootLayout = new VerticalLayout("box");

            var editorView = EasyIMGUI.Vertical().Parent(RootLayout);
            var uploadingView = new VerticalLayout().Parent(RootLayout);

            // 当前版本号
            var versionLine = EasyIMGUI.Horizontal().Parent(editorView);
            EasyIMGUI.Label().Text("当前版本号").Width(100).Parent(versionLine);
            EasyIMGUI.Label().Text(mPackageVersion.Version).Width(100).Parent(versionLine);

            // 发布版本号 
            var publishedVersionLine = EasyIMGUI.Horizontal().Parent(editorView);

            EasyIMGUI.Label().Text("发布版本号")
                .Width(100)
                .Parent(publishedVersionLine);

            EasyIMGUI.TextField()
                .Text(mPublishVersion)
                .Width(100)
                .Parent(publishedVersionLine)
                .Content.Register(v => mPublishVersion = v);

            // 类型
            var typeLine = EasyIMGUI.Horizontal().Parent(editorView);
            EasyIMGUI.Label().Text("类型").Width(100).Parent(typeLine);

            var packageType = EasyIMGUI.EnumPopup(mPackageVersion.Type).Parent(typeLine);

            var accessRightLine = EasyIMGUI.Horizontal().Parent(editorView);
            EasyIMGUI.Label().Text("权限").Width(100).Parent(accessRightLine);
            var accessRight = EasyIMGUI.EnumPopup(mPackageVersion.AccessRight).Parent(accessRightLine);

            EasyIMGUI.Label().Text("发布说明:").Width(150).Parent(editorView);

            var releaseNote = EasyIMGUI.TextArea().Width(245)
                .Parent(editorView);

            // 文件选择部分
            EasyIMGUI.Label().Text("插件目录: " + mPackageVersion.InstallPath)
                .Parent(editorView);

            EasyIMGUI.Custom().OnGUI(() =>
            {
                mScrollPosition = EditorGUILayout.BeginScrollView(mScrollPosition);

                mAssetTreeGUI.DrawTreeLayout();

                EditorGUILayout.EndScrollView();
            }).Parent(editorView);


            InEditorView.RegisterWithInitValue(value => { editorView.Visible = value; })
                .AddToUnregisterList(this);

            EasyIMGUI.Button()
                .Text("迁移")
                .OnClick(() =>
                {
                    var includedPaths = new List<string>();
                    mAssetTree.Root.Traverse(data =>
                    {
                        if (data != null && data.isSelected)
                        {
                            includedPaths.Add(data.fullPath);
                            return false;
                        }

                        return true;
                    });


                    mPackageVersion.IncludeFileOrFolders = includedPaths;
                    mPackageVersion.Readme.content = releaseNote.Content.Value;
                    mPackageVersion.AccessRight = (PackageAccessRight)accessRight.ValueProperty.Value;
                    mPackageVersion.Type = (PackageType)packageType.ValueProperty.Value;
                    mPackageVersion.Version = mPublishVersion;
                    this.SendCommand(new MovePackageCommand(mPackageVersion));
                }).Parent(editorView);

            var notice = EasyIMGUI.LabelWithRect()
                .Rect(new Rect(100, 200, 200, 200))
                .Parent(uploadingView);

            NoticeMessage
                .RegisterWithInitValue(value => { notice.Text(value); }).AddToUnregisterList(this);

            InUploadingView.RegisterWithInitValue(value => { uploadingView.Visible = value; })
                .AddToUnregisterList(this);
        }

        public IArchitecture GetArchitecture()
        {
            return AlphaToAdminArchitecture.Interface;
        }

        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
        
        public override void OnGUI()
        {
            base.OnGUI();

            RootLayout.DrawGUI();

            RenderEndCommandExecutor.ExecuteCommand();
        }
    }

    public class AlphaToAdminArchitecture : Architecture<AlphaToAdminArchitecture>
    {
        protected override void Init()
        {
        }
    }


    internal class MovePackageCommand : AbstractCommand
    {
        private readonly PackageVersion mPackageVersion;

        public MovePackageCommand(PackageVersion packageVersion)
        {
            mPackageVersion = packageVersion;
        }

        protected override void OnExecute()
        {
            if (mPackageVersion.Readme.content.Length < 2)
            {
                IMGUIHelper.ShowEditorDialogWithErrorMsg("请输入版本修改说明");
                return;
            }

            if (!IsVersionValid(mPackageVersion.Version))
            {
                IMGUIHelper.ShowEditorDialogWithErrorMsg("请输入正确的版本号 格式:vX.Y.Z");
                return;
            }

            mPackageVersion.DocUrl = "https://liangxiegame.com";

            mPackageVersion.Readme = new ReleaseItem(mPackageVersion.Version, mPackageVersion.Readme.content,
                "liangxie",
                DateTime.Now);

            mPackageVersion.Save();

            AssetDatabase.Refresh();

            RenderEndCommandExecutor.PushCommand(() => { PublishPackage(mPackageVersion, false); });
        }

        public void PublishPackage(PackageVersion packageVersion, bool deleteLocal)
        {
            AlphaToAdmin.NoticeMessage.Value = "插件上传中,请稍后...";

            AlphaToAdmin.InUploadingView.Value = true;
            AlphaToAdmin.InEditorView.Value = false;
            AlphaToAdmin.InFinishView.Value = false;

            DoMove(packageVersion, () =>
            {
                // if (deleteLocal)
                // {
                    // Directory.Delete(packageVersion.InstallPath, true);
                    // AssetDatabase.Refresh();
                // }

                AlphaToAdmin.UpdateResult.Value = "迁移成功";

                AlphaToAdmin.InEditorView.Value = false;
                AlphaToAdmin.InUploadingView.Value = false;
                AlphaToAdmin.InFinishView.Value = true;

                if (EditorUtility.DisplayDialog("上传结果", AlphaToAdmin.UpdateResult.Value, "OK"))
                {
                    AssetDatabase.Refresh();

                    EditorWindow.GetWindow<AlphaToAdmin>().Close();
                }
            });
        }

        public static bool IsVersionValid(string version)
        {
            if (version == null)
            {
                return false;
            }

            var t = version.Split('.');
            return t.Length == 3 && version.StartsWith("v");
        }

        private static string UPLOAD_URL
        {
            get { return "https://api.liangxiegame.com/qf/v4/package/add"; }
        }

        public static void DoMove(PackageVersion packageVersion, System.Action succeed)
        {
            EditorUtility.DisplayProgressBar("插件迁移", "打包中...", 0.1f);

            var fileName = packageVersion.Name + "_" + packageVersion.Version + ".unitypackage";
            var fullPath = ExportPaths(fileName, packageVersion.IncludeFileOrFolders.ToArray());
            // var file = File.ReadAllBytes(fullPath);

            // var form = new WWWForm();
            // form.AddField("username", User.Username.Value);
            // form.AddField("password", User.Password.Value);
            // form.AddField("name", packageVersion.Name);
            // form.AddField("version", packageVersion.Version);
            // form.AddBinaryData("file", file);
            // form.AddField("releaseNote", packageVersion.Readme.content);
            // form.AddField("installPath", packageVersion.InstallPath);
            //
            // form.AddField("accessRight", packageVersion.AccessRight.ToString().ToLower());
            // form.AddField("docUrl", packageVersion.DocUrl);
            //
            // if (packageVersion.Type == PackageType.FrameworkModule)
            // {
            //     form.AddField("type", "fm");
            // }
            // else if (packageVersion.Type == PackageType.Shader)
            // {
            //     form.AddField("type", "s");
            // }
            // else if (packageVersion.Type == PackageType.AppOrGameDemoOrTemplate)
            // {
            //     form.AddField("type", "agt");
            // }
            // else if (packageVersion.Type == PackageType.Plugin)
            // {
            //     form.AddField("type", "p");
            // }

            // Debug.Log(fullPath);

            // EditorUtility.DisplayProgressBar("插件上传", "上传中...", 0.2f);

            // EditorHttp.Post(UPLOAD_URL, form, (response) =>
            // {
                // if (response.Type == ResponseType.SUCCEED)
                // {
                    EditorUtility.ClearProgressBar();
                    // Debug.Log(response.Text);
                    // if (succeed != null)
                    // {
                        succeed();
                    // }

                    // File.Delete(fullPath);
                // }
                // else
                // {
                    // EditorUtility.ClearProgressBar();
                    // EditorUtility.DisplayDialog("插件上传", string.Format("上传失败!{0}", response.Error), "确定");
                    // File.Delete(fullPath);
                // }
            // });
        }

        private static readonly string EXPORT_ROOT_DIR = Path.Combine(Application.dataPath, "QFramework/Admin/Editor/");

        public static string ExportPaths(string exportPackageName, params string[] paths)
        {
            var filePath = Path.Combine(EXPORT_ROOT_DIR, exportPackageName);

            filePath.DeleteFileIfExists();

            AssetDatabase.ExportPackage(paths, filePath, ExportPackageOptions.Recurse);
            AssetDatabase.Refresh();
            return filePath;
        }
    }
}
#endif