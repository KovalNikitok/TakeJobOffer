import { Menu } from "antd";
import { Header } from "antd/es/layout/layout";

interface Item {
    key: string;
    label: JSX.Element;
}

interface Props {
    itemsLeft: Item[];
    itemsRight: Item[];
}

export const HeaderLayout = ( { itemsLeft, itemsRight } : Props) => {
    return (
        <Header className="header__layout">
            <Menu
              theme="dark"
              mode="horizontal"
              items={itemsLeft}
              className="menu__layout"
            />
            <Menu
              theme="dark"
              mode="horizontal"
              items={itemsRight}
              className="menu__layout menu_layout_right"
            />
          </Header>
    );
}