using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MyApp
{
    // 该类型的消息用于通知主窗体客户修改了订单内容，需要重新计算价格并刷新屏幕显示
    public sealed class CustomerOrderChangedMessage : ValueChangedMessage<string>
    {
        public CustomerOrderChangedMessage(string value) : base(value)
        {
        }

        public override string ToString()
        {
            string messageType = typeof(CustomerOrderChangedMessage).FullName;
            return $"{messageType}: '{Value}'";
        }
    }
}
