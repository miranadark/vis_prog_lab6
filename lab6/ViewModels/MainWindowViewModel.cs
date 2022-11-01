using System;
using System.Collections.Generic;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using visual_programming_lab6.Models;

namespace visual_programming_lab6.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public List<Note> ItemsAll { get; set; }
        public MainWindowViewModel()
        {
            Content = fv = new FirstViewModel(BuildAllNotes());
            AddButton = ReactiveCommand.Create<Unit, Unit>(
                (unit) =>
                {
                    var newItem = new Note("", "", fv.Currentdate);
                    var sv = new SecondViewModel(newItem);
                    Observable.Merge(
                sv.OKButton,
                sv.CancelButton.Select(_ => Unit.Default))
                .Take(1)
                .Subscribe(unit =>
                {
                    if (newItem.Name != "")
                        ItemsAll.Add(newItem);
                    fv.changeItems();
                    Content = fv;
                });
                    Content = sv;
                    return Unit.Default;
                });

            OpenButton = ReactiveCommand.Create<Note, Unit>(
                (item) =>
                {
                    var sv = new SecondViewModel(item);
                    Observable.Merge(
                sv.OKButton,
                sv.CancelButton.Select(_ => Unit.Default))
                .Take(1)
                .Subscribe(unit =>
                {
                    fv.changeItems();
                    Content = fv;
                });
                    Content = sv;
                    return Unit.Default;
                });

            DeleteButton = ReactiveCommand.Create<Note, Unit>((item) =>
            {
                ItemsAll.Remove(item);
                fv.changeItems();
                return Unit.Default;
            });
            ItemsAll = BuildAllNotes();
            Content = fv = new FirstViewModel(ItemsAll);
        }

        ViewModelBase content;
        public ViewModelBase Content
        {
            set => this.RaiseAndSetIfChanged(ref content, value);
            get => content;
        }

        public FirstViewModel fv
        {
            get;
        }

        public ReactiveCommand<Unit, Unit> AddButton { get; }
        public ReactiveCommand<Note, Unit> OpenButton { get; }
        public ReactiveCommand<Note, Unit> DeleteButton { get; }

        private List<Note> BuildAllNotes()
        {
            return new List<Note>
            {
                new Note("Заголовок Дела0","Список0", DateTime.Today.AddDays(-1)),
                new Note("Заголовок Дела1", "Список1", DateTime.Today),
                new Note("Заголовок Дела2", "Список2", DateTime.Today),
                new Note("Заголовок Дела3", "Список3", DateTime.Today.AddDays(1)),
                new Note("Заголовок Дела4", "Список4", DateTime.Today.AddDays(1)),
            };
        }
    }
}