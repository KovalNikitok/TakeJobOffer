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
  { key: "professions", label: <Link href={"/professions"}>Профессии</Link> },
  { key: "skills", label: <Link href={"/skills"}>Навыки</Link> },
  { key: "professionsSkills", label: <Link href={"/professions-skills"}>Требования по навыкам</Link> },
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