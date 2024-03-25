import { Layout, Menu } from "antd";
import "./globals.css";
import { Content, Footer, Header } from "antd/es/layout/layout";
import Link from "antd/es/typography/Link";

const items = [
  { key: "home", label: <Link href={"/"}>Home</Link> },
  { key: "professions", label: <Link href={"/professions"}>Professions</Link> },
];

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <Layout style={{ minHeight: "100vh" }}>
          <Header>
            <Menu
              theme="dark"
              mode="horizontal"
              items={items}
              style={{ flex: 1, minHeight: 0 }}
            />
          </Header>
          <Content style={{ padding: "0 48px" }}>{children}</Content>
          <Footer style={{ textAlign: "center" }}>
            TakeJobOffer 2024 Created by Nikita Koval
          </Footer>
        </Layout>
      </body>
    </html>
  );
}
