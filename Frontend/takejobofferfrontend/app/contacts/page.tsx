<<<<<<< Updated upstream
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTelegram, faVk, faGithub, faDiscord } from '@fortawesome/free-brands-svg-icons';
import Link from "antd/es/typography/Link";
import { Content } from 'antd/es/layout/layout';
=======
import "./contacts.page.css";

import Link from "antd/es/typography/Link";
import { Content } from 'antd/es/layout/layout';
import { List } from "antd";
>>>>>>> Stashed changes

export default function ProfessionsPage() {
    const linksHeight = 100;
    return (
<<<<<<< Updated upstream
        <Content style={{ padding: '10px', textAlign: 'center', display: 'grid;'}}>
            <div className="contacts__div">
                <Link href="https://discord.com/users/303456093726572544">
                        <FontAwesomeIcon icon={faDiscord} height={linksHeight}/>
                </Link>
                <Link href="https://vk.com/duosrx">
                    <FontAwesomeIcon icon={faVk} height={linksHeight}/>
                </Link>
                <Link href="https://t.me/twicereader">
                    <FontAwesomeIcon icon={faTelegram} height={linksHeight}/>
                </Link>
                <Link href="https://github.com/KovalNikitok">
                    <FontAwesomeIcon icon={faGithub} height={linksHeight}/>
                </Link>
            </div>
        </Content>
=======
      <Content className="contacts__content">
        <h2 className="contacts__h2">
          Хотите связаться с автором?
          <br />
          Обращайтесь любым удобным способом!
        </h2>
        <Link
          className="contacts__link-mailto"
          href="mailto:nikitakovalmen2@gmail.com"
        >
          <h3 className="contacts__h3">
            nikitakovalmen2@gmail.com
          </h3>
        </Link>
        <List className="contacts__list">
          <Link
            className="contacts__link-sn"
            href="https://discord.com/users/303456093726572544"
          >
            Discord
          </Link>
          <Link className="contacts__link-sn" href="https://vk.com/duosrx">
            VK
          </Link>
          <Link className="contacts__link-sn" href="https://t.me/twicereader">
            Telegram
          </Link>
          <Link
            className="contacts__link-sn"
            href="https://github.com/KovalNikitok"
          >
            Github
          </Link>
        </List>
      </Content>
>>>>>>> Stashed changes
    );
}