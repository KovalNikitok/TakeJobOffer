import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTelegram, faVk, faGithub, faDiscord } from '@fortawesome/free-brands-svg-icons';
import Link from "antd/es/typography/Link";
import { Content } from 'antd/es/layout/layout';

export default function ProfessionsPage() {
    const linksHeight = 100;
    return (
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
    );
}