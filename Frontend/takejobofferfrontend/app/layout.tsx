import "./globals.css";
import { Source_Sans_3 } from 'next/font/google'

import { Layout } from "antd";

import { Content } from "antd/es/layout/layout";
import { AntdRegistry } from '@ant-design/nextjs-registry';
import { HeaderLayout } from "./components/HeaderLayout";
import { FooterLayout } from "./components/FooterLayout";

import Link from "antd/es/typography/Link";

  
const items = [
  { key: "home", label: <Link href={"/"}>TakeJobOffer</Link> },
  { key: "professionsSkills", label: <Link href={"/professions"}>Требования по навыкам</Link> },
  //{ key: "professions", label: <Link href={"/admin-professions"}>Профессии</Link> },
  //{ key: "skills", label: <Link href={"/admin-skills"}>Навыки</Link> },
];

const ss3 = Source_Sans_3({ 
  weight: '400',
  subsets: ['greek']
});

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
            <HeaderLayout items={items}></HeaderLayout>
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