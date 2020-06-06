﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Livet;
using TimeRecorder.Domain.Domain.Tasks;
using TimeRecorder.Domain.Domain.Tracking;
using TimeRecorder.Domain.UseCase.Tasks;
using TimeRecorder.Domain.UseCase.Tracking;
using TimeRecorder.Domain.Utility;
using TimeRecorder.Host;

namespace TimeRecorder.Contents.WorkUnitRecorder.Tasks
{
    public class WorkTaskModel
    {
        private readonly WorkTaskUseCase _WorkTaskUseCase;
        private readonly WorkingTimeRangeUseCase _WorkingTimeRangeUseCase;

        public WorkTaskModel()
        {
            _WorkTaskUseCase = new WorkTaskUseCase(ContainerHelper.Resolver.Resolve<IWorkTaskRepository>(), ContainerHelper.Resolver.Resolve<IWorkingTimeRangeRepository>());

            _WorkingTimeRangeUseCase = new WorkingTimeRangeUseCase(
                                            ContainerHelper.Resolver.Resolve<IWorkingTimeRangeRepository>(),
                                            ContainerHelper.Resolver.Resolve<IWorkTaskRepository>());
        }

        public void EditWorkTask(WorkTask workTask)
        {
            _WorkTaskUseCase.Edit(workTask);
            ObjectChangedNotificator.Instance.NotifyWorkTaskEdited();
        }

        public void CompleteWorkTask(Identity<WorkTask> id)
        {
            _WorkTaskUseCase.Complete(id);
            ObjectChangedNotificator.Instance.NotifyWorkTaskEdited();
        }

        public void DeleteWorkTask(Identity<WorkTask> id)
        {
            _WorkTaskUseCase.Delete(id);
            ObjectChangedNotificator.Instance.NotifyWorkTaskEdited();
        }

        public void StartWorking(Identity<WorkTask> id)
        {
            _WorkingTimeRangeUseCase.StartWorking(id);
            ObjectChangedNotificator.Instance.NotifyWorkTaskEdited();
        }

        public void StopWorking(Identity<WorkingTimeRange> id)
        {
            _WorkingTimeRangeUseCase.StopWorking(id);
            ObjectChangedNotificator.Instance.NotifyWorkTaskEdited();
        }

        public void EditWorkingTime(WorkingTimeRange target)
        {
            _WorkingTimeRangeUseCase.EditWorkingTimeRange(target.Id, target.TimePeriod.StartDateTime, target.TimePeriod.EndDateTime);
            ObjectChangedNotificator.Instance.NotifyWorkTaskEdited();
        }

        public void AddWorkingTime(WorkingTimeRange target)
        {
            _WorkingTimeRangeUseCase.AddWorkingTimeRange(target);
            ObjectChangedNotificator.Instance.NotifyWorkTaskEdited();
        }

        public void DeleteWorkingTime(WorkingTimeRange target)
        {
            _WorkingTimeRangeUseCase.DeleteWorkingTimeRange(target.Id);
            ObjectChangedNotificator.Instance.NotifyWorkTaskEdited();
        }



        public WorkTask SelectWorkTask(Identity<WorkTask> identity)
        {
            return _WorkTaskUseCase.SelectById(identity);
        }
    }
}
