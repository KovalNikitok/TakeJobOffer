import "./profession-table.page.css";

import { getAllProfessionsWithSlugISR } from "../../shared/api/professions";

import ProfessionSkillsClient from "./ui/ProfessionSkillsClient";

export const revalidate = 600;
export const dynamicParams = true;

export async function generateStaticParams () {
  let professionsWithslug: ProfessionWithSlug[];
  try {
    professionsWithslug = await getAllProfessionsWithSlugISR(revalidate);
    console.log(professionsWithslug);
    
    return professionsWithslug.map(profession => ({
      params: { slug: profession.slug },
    }));
  }
  catch (error) {
    console.log("Profession not found" + `\n ${error}`);
    return [{ params: { slug: "" }}];
  }
}

export default async function ProfessionSkillsPage({ params }: { params: { slug: string } }) {
  const { slug } = params;

  return (
    <ProfessionSkillsClient slug={slug} />
  );
}