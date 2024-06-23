import "./profession-table.page.css";

import { getAllProfessionsWithSlugISR, getProfessionBySlug } from "../../shared/api/professions";

import ProfessionSkillsClient from "./ui/ProfessionSkillsClient";

export const revalidate = 600;
export const dynamicParams = true;

export async function generateStaticParams () {
  let professionsWithslug: ProfessionWithSlug[];
  try {
    professionsWithslug = await getAllProfessionsWithSlugISR(revalidate);
    
    return professionsWithslug.map(profession => ({
      params: { slug: profession.slug },
    }));
  }
  catch (error) {
    console.log("Professions not found" + `\n ${error}`);
    return [{ params: { slug: "" }}];
  }
}

export async function generateMetadata ({ params }: { params: { slug: string } }) {
  let professionsWithSlug = await getProfessionBySlug(params.slug);

  return {
    title: `Требования по навыкам на позицию ${professionsWithSlug.description}`,
    description: `Узнайте требования по навыкам на позицию ${professionsWithSlug.description} и составьте продающее резюме!`,
    openGraph: {
        title: `Требования по навыкам на позицию ${professionsWithSlug.description}`,
        description: `Узнайте требования по навыкам на позицию ${professionsWithSlug.description} и составьте продающее резюме!`,
        url: `https://takejoboffer.ru/professions/${params.slug}`
    },
    keywords: ['TakeJobOffer', `${professionsWithSlug.name}`, `${professionsWithSlug.description}`,'работа', 'вакансии', 'поиск вакансий', 'резюме', 'работы', 'работу', 'работ', 'ищу работу', 'поиск', 'поиск работы', 'навыки для работы', 'навыки для резюме', 'навыки в резюме', 'it работа', 'составление резюме'],
  };
}

export default async function ProfessionSkillsPage({ params }: { params: { slug: string } }) {
  const { slug } = params;

  return (
    <ProfessionSkillsClient slug={slug} />
  );
}