import Spin from "antd/es/spin";

const contentStyle: React.CSSProperties = {
    padding: 50,
    background: 'rgba(0, 0, 0, 0.05)',
    borderRadius: 4,
  };
  
  const content = <div style={contentStyle} />;

export const Loading = () => {
    return (
        <Spin className="loader__spin" size="large" tip="Загрузка...">
            {content}
        </Spin>  
    );
};