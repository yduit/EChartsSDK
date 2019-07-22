using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ECharts
{
    /// <summary>
    /// 重载返回JSON格式(obj2json)
    /// </summary>
    public class EChartJsonResult : JsonResult
    {
        private string dateFormatString = "yyyy-MM-dd HH:mm:ss";
        private JsonSerializerSettings jsonSerializerSettings = null;

        public EChartJsonResult()
        {
        }

        public EChartJsonResult(string dateFormatString)
        {
            this.dateFormatString = dateFormatString;
        }

        public EChartJsonResult(JsonSerializerSettings jsonSerializerSettings)
        {
            this.jsonSerializerSettings = jsonSerializerSettings;
        }

        /// <summary>
        /// 重载返回JSON格式(obj2json)
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            JsonConvert.DefaultSettings = () =>
            {
                return jsonSerializerSettings ?? new JsonSerializerSettings()
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace,//返回或者设置反序列化期间如何创建对象。
                    MissingMemberHandling = MissingMemberHandling.Ignore,//get和set缺失的成员(例如JSON包含一个属性,并不是一个成员对象)在反序列化处理。
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,//获取或设置如何引用循环处理(例如,一个类引用本身)。
                    NullValueHandling = NullValueHandling.Ignore,//控制处理
                    DateFormatString = dateFormatString,//时间处理
                };
            };
            response.Write(JsonConvert.SerializeObject(
                this.Data,
                new StringEnumConverter()));
        }

    }
    /// <summary>
    /// 重载返回JSON格式(jsonString2json)
    /// </summary>
    public class EChartJsonStringResult : JsonResult
    {
        /// <summary>
        ///  重载返回JSON格式(jsonString2json)
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            response.Write(this.Data.ToString());
        }
    }
}
