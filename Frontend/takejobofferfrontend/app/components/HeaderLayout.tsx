import { Header } from "antd/es/layout/layout";
import { Menu } from "antd";

interface Item {
    key: string;
    label: JSX.Element;
}

interface Props {
    items: Item[];
}

export const HeaderLayout = ( { items } : Props) => {
    return (
        <Header className="header__layout">
            <Menu
              theme="dark"
              mode="horizontal"
              items={items}
              className="menu__layout"
            />
          </Header>
    );
}