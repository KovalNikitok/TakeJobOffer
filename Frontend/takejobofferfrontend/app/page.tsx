import './home.page.css';

import { Image, Button, Row, Col } from 'antd';
import { Content } from 'antd/es/layout/layout';
import Link from "antd/es/typography/Link";
import Paragraph from 'antd/es/typography/Paragraph';
import Title from 'antd/es/typography/Title';



export default function Home() {
  return (
    <Content style={{ padding: '10px', textAlign: 'center' }}>
        <Title level={1}>TakeJobOffer</Title>
        <Paragraph style={{ fontSize: '18px' }}>
          Найдите лучшую работу в IT сфере.
        </Paragraph>
        <Paragraph style={{ fontSize: '16px', color: 'rgba(0, 0, 0, 0.65)' }}>
          Наш сайт поможет вам найти вакансии и узнать актуальные требования по навыкам для различных профессий в IT.
        </Paragraph>
        <Button 
          type="primary" 
          size="large" 
          className="redirect-button"
        >
          <Link href="/professions">Начни поиски работы с малого</Link>
        </Button>
        <Row gutter={16} style={{ marginTop: '40px' }}>
          <Col xs={24} sm={12}>
            <Link href="/professions">
              <Image 
                src="https://img.freepik.com/premium-photo/group-people-collaborating-working-together-around-laptop-table_956369-4884.jpg"
                alt="Профессии"
                className="image-professions"
                preview={false}
              >
              </Image>
            </Link>
          </Col>
          <Col xs={24} sm={12}
            className='home-col'
          >
            <Title level={4}>Для чего можно использовать наш сайт?</Title>
            <Paragraph style={
              { 
                fontSize: '16px', 
                color: 'rgba(0, 0, 0, 0.65)'
              }}>
              - Помощь в поиске вакансий по навыкам
              <br />
              - Помощь в составлении релевантного резюме
              <br />
              - Подготовка к собеседованиям
              <br />
              <p>
                Вам предоставлена актуальная информация из надёжных источников
              </p>
            </Paragraph>
          </Col>
        </Row>
      </Content>
  );
}
