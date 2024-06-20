import { Metadata, Viewport } from "next";

export const metadata: Metadata = {
    icons: [
        {
            rel: 'apple-touch-icon',
            sizes: '180x180',
            url: '/apple-touch-icon.png',
        },
        {
            rel: 'icon',
            type: 'image/png',
            sizes: '32x32',
            url: '/favicon-32x32.png',
        },
        {
            rel: 'icon',
            type: 'image/png',
            sizes: '16x16',
            url: '/favicon-16x16.png',
        },
        {
            rel: 'mask-icon',
            color: '#5bbad5',
            url: '/safari-pinned-tab.svg',
        },
        {
            rel: 'icon',
            type: 'image/x-icon',
            sizes: '48x48',
            url: '/favicon.ico',
        },
    ],
    manifest: '/manifest.webmanifest',
    keywords: ['работа', 'вакансии', 'поиск работы', 'профессия', 'профессии', 'поиск вакансий', 'резюме', 'работы', 'работу', 'работ', 'ищу работу', 'поиск', 'it работа', 'составление резюме'],
    openGraph: {
        siteName: 'TakeJobOffer',
        images: [
            {
                url: 'https://img.freepik.com/premium-photo/group-people-collaborating-working-together-around-laptop-table_956369-4884.jpg',
                width: 480,
                height: 320,
                alt: 'Требования для профессий по навыкам',
            },
        ],
        locale: 'ru_RU',
        type: 'website'
    }
};

export const viewport: Viewport = {
    themeColor: '#343a40',
    width: 'device-width',
    initialScale: 1.0,
    userScalable: false
}