using System;
using EGO.Framework;
using EGO.V1;
using UnityEngine;

namespace EGO.ViewController
{
    public class NoteView : HorizontalLayout
    {
        public NoteView(Note note,Action<Note> OpenProcessWindow,Action onEdit)
        {
            new ImageButtonView("edit", onEdit)
                .Width(25)
                .Height(25)
                .Color(Color.black)
                .AddTo(this);

            new LabelView(note.Content).Height(25).FontSize(15).TextMiddleLeft().AddTo(this);

            new ImageButtonView("process", () =>
                {
                    this.PushCommand(() =>
                    {
                        OpenProcessWindow(note); 
                    }); 
                })
                .Width(25)
                .Height(25)
                .Color(Color.blue)
                .AddTo(this);

            new ImageButtonView("delete", () =>
            {
                Model.RemoveNote(note);
                Model.Save();

                this.PushCommand(RemoveFromParent);

            }).Width(25).Height(25).Color(Color.red).AddTo(this);
        }
    }
}