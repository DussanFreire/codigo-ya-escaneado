using AutoMapper;
using BackEndAlbergue.Data.Entities;
using BackEndAlbergue.Data.Repository;
using BackEndAlbergue.Exceptions;
using BackEndAlbergue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAlbergue.Services
{
    public class NoticeService : INoticeService
    {
        private IRefuge _refugeRepository;
        private IMapper _mapper;

        public NoticeService(IRefuge refugeRepository, IMapper _mapper)
        {
            this._refugeRepository = refugeRepository;
            this._mapper = _mapper;
        }
        public NoticeModel GetNotice(int noticeId)
        {
            var notice = _refugeRepository.GetNotice(noticeId);

            if (notice == null)
            {
                throw new NotFoundException($"The pet {noticeId} doesnt exists, try it with a new id");
            }

            return _mapper.Map<NoticeModel>(notice);
        }
        public IEnumerable<NoticeModel> GetNotices()
        {
            var entityList = _refugeRepository.GetNotices();
            var modelList = _mapper.Map<IEnumerable<NoticeModel>>(entityList);
            return modelList;
        }
        public NoticeModel CreateNotice(NoticeModel noticeModel)
        {

            var entityreturned = _refugeRepository.CreateNotice(_mapper.Map<NoticeEntity>(noticeModel));

            return _mapper.Map<NoticeModel>(entityreturned);
        }
        public bool DeleteNotice(int noticeId)
        {
            var validation = GetNotice(noticeId);
            return _refugeRepository.DeleteNotice(noticeId);
        }

        public NoticeModel UpdateNotice(int noticeId, NoticeModel noticeModel)
        {
            noticeModel.id = noticeId;
            var noticeToUpdate = _refugeRepository.UpdateNotice(_mapper.Map<NoticeEntity>(noticeModel));
            return _mapper.Map<NoticeModel>(noticeToUpdate);
        }
    }
}
