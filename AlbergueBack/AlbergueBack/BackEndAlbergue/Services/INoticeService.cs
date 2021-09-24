using BackEndAlbergue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAlbergue.Services
{
    public interface INoticeService
    {
        public IEnumerable<NoticeModel> GetNotices();
        public NoticeModel GetNotice(int noticeId);
        public NoticeModel CreateNotice(NoticeModel noticeModel);
        public bool DeleteNotice(int noticeId);
        public NoticeModel UpdateNotice(int noticeId, NoticeModel noticeModel);
    }
}
