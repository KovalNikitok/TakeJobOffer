import { Header } from "antd/es/layout/layout";
import { Menu } from "antd";

interface Item {
    key: string;
    label: JSX.Element;
}

// interface Props {
//     items: Item[];
// }

// export const HeaderLayout = ( { items } : Props) => {
//     return (
//         <Header className="header__layout">
//             <Menu
//               theme="dark"
//               mode="horizontal"
//               items={items}
//               className="menu__layout"
//             />
//           </Header>
//     );
// }

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