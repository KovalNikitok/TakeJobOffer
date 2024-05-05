import { getAllProfessionsWithSlugISR } from "../../services/professions";
import ProfessionSkillsClient  from "./ProfessionSkillsPage";

export const revalidate = 600;
export const dynamicParams = true;

export async function generateStaticParams () {
  let professionsWithslug: ProfessionWithSlug[];
  try {
    professionsWithslug = await getAllProfessionsWithSlugISR(revalidate);
    return professionsWithslug.map(profession => ({
      params: { professionSlug: profession.slug },
    }));
  }
  catch (error) {
    console.log("Profession not found" + `\n ${error}`);
    return [{ params: { professionSlug: "" }}];
  }
}

export default async function ProfessionSkillsPage({ params }: { params: { professionSlug: string } }) {
  const { professionSlug } = params;

  return (
    <ProfessionSkillsClient professionSlug={professionSlug} />
  );
}