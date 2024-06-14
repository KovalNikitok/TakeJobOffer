import { Footer } from "antd/es/layout/layout";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTelegram, faVk, faGithub, faDiscord } from '@fortawesome/free-brands-svg-icons';
import Link from "antd/es/typography/Link";

export const FooterLayout = () => {
    const linksHeight = 30;
    return (
        <Footer className="footer__layout">      
            <div className="footer__div-refs">
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
            <h5 className="footer__h5"> 
                © 2024 TakeJobOffer by Nikita Koval – All rights reserved
            </h5>
        </Footer>
    );
}