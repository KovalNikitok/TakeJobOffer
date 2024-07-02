import "./globals.css";

import { Source_Sans_3 } from 'next/font/google'
import { Layout } from "antd";
import { Content } from "antd/es/layout/layout";
import { AntdRegistry } from '@ant-design/nextjs-registry';
import { HeaderLayout } from "./shared/ui/HeaderLayout";
import { FooterLayout } from "./shared/ui/FooterLayout";
import { metadata, viewport } from "./shared/ui/HeadComponent";
import Link from "antd/es/typography/Link";

const itemsLeft = [
  { key: "home", label: <Link href={"/"}>TakeJobOffer</Link> },
  { key: "professionsSkills", label: <Link href={"/professions"}>Требования по навыкам</Link> },
];

const itemsRight= [
  { key: "contacts", label: <Link href={"/contacts"}>Контакты</Link> },
  //{ key: "about", label: <Link href={"/about"}>О нас</Link> },
];

const ss3 = Source_Sans_3({ 
  weight: '400',
  subsets: ['greek']
});

export { metadata, viewport };

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="ru">
      <body className={ss3.className}>  
        <AntdRegistry>
          <Layout className="layout">
            <HeaderLayout itemsLeft={itemsLeft} itemsRight={itemsRight}></HeaderLayout>
            <Content className="content">
              {children}
            </Content>
            <FooterLayout></FooterLayout>
          </Layout>
        </AntdRegistry>
      </body>
    </html>
  );
}