using EGO.Constant;
using EGO.Framework;
using EGO.Util;
using EGO.V1;

namespace EGO.ViewController
{
    public class NoteEditorView : VerticalLayout
    {
        
        public NoteEditorView(Note model = null)
        {
            VerticalStyle = "box";
            
            var content = model == null ? string.Empty : model.Content;

            new TextAreaView(content)
                .ExpandHeight()
                .FontSize(15)
                .AddTo(new ScrollLayout().AddTo(this))
                .Content.Bind(newContent => content = newContent);

            new ButtonView("保存", () =>
            {
                if (model == null)
                {
                    Model.CreateNote(content);
                }
                else
                {
                    model.Content = content;
                }

                Model.Save();

                this.PushCommand(() =>
                {
                    new NoteListView().AddTo(Parent); 
                });
                
                RemoveFromParent();

            }).AddTo(this);

            RegisterEvent(GlobalEvents.OnTopBarMenuClicked, menuKey =>
            {
                if (menuKey == NoteListView.Key)
                {
                    Show();
                }
                else
                {
                    Hide();
                }
            });
        }
    }
}